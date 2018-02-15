using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace MyWebsite
{
    public class GeoLocator
    {
        public static void Locate(RequestContext requestContext)
        {
            try
            {
                var requestBase = requestContext.HttpContext.Request;
                var ipAddress = requestBase.UserHostAddress;

                string trackingInfo = null;

                if (ipAddress == null) return;

                if (ipAddress == "::1")
                    trackingInfo = "Its You";
                else
                {
                    var requestUrl = $"{Properties.Settings.Default.GeoIPUrl}/{ipAddress}";

                    var request = WebRequest.Create(requestUrl);
                    var response = request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);

                    var serializer = new JavaScriptSerializer();

                    trackingInfo = streamReader.ReadToEnd();

                }
                if(trackingInfo!=null)
                {
                    using (var ctx = new StudentEntities())
                    {
                        var tracker = ctx.Trackers.Where(t => t.IpAddress.Trim() == ipAddress.Trim()).FirstOrDefault();
                        if (tracker != null)
                        {
                            tracker.NoOfHits = tracker.NoOfHits + 1;
                            tracker.LastVisitedOn = DateTime.Now;
                            tracker.TrackingInfo = trackingInfo;
                        }
                        else
                        {
                            ctx.Trackers.Add(new Tracker()
                            {
                                TrackingInfo = trackingInfo,
                                NoOfHits = 1,
                                LastVisitedOn = DateTime.Now,
                                IpAddress = ipAddress.Trim()
                            });
                        }
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}