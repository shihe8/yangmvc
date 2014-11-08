using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using System.IO;

using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
namespace MvcApplication1.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            //WebClient wc = new WebClient();
            //try
            //{
            //    object o = new object();

            //    for (int i = 21103; i < 12333330; i++)
            //    {
            //        wc = new WebClient();
            //        lock (o)
            //        {
            //            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0");
            //            wc.Headers.Add("Cookie", "JSESSIONID=732D21A4D51BF2D90B68B189FF132F55");
            //            wc.Headers.Add("Content-type", "application/x-www-form-urlencoded");


            //            Response.Write(wc.DownloadString("http://bg.ny.cn/companyRegisterAction!deleteCompanys.action?companyRegisterId=" + i.ToString()));



            //        }
            //    }


            //}
            //catch (Exception er)
            //{ throw new Exception(er.Message); }
            //finally
            //{
            //    wc.Dispose();
            //}

           using(MemcachedClient client=new MemcachedClient()){
             Response.Write( client.Store(StoreMode.Set, "u1", new MvcApplication1.Models.UnderWear {  Uid=123, UderWearName="古今内衣", AddTime=new DateTime(2009,10,21)}).ToString());

           }
             
            return null;
        }
        public ActionResult UpFile()
        {
            WebClient wclient = new WebClient();
            //FileStream fs = new FileStream(@"D:\2.jpg", FileMode.Open, FileAccess.Read);
            try
            {
               //wclient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0");
                 
                wclient.Headers.Add("Content-type", "application/x-www-form-urlencoded");
                //byte[] mydata = new byte[fs.Length];
                //fs.Read(mydata, 0, mydata.Length);
           
                //wclient.UploadData("http://192.168.0.110/FileSaveIt.ashx", "post", mydata);
                wclient.UploadData("http://192.168.0.110/FileSaveIt.ashx","post",Encoding.UTF8.GetBytes("mytime=11爱你在——"+DateTime.Now.ToString()+"&jj=整整之外"));
                
            }
            catch (Exception er) { throw new Exception(er.Message); }
            finally
            {

                //fs.Close();
                wclient.Dispose();
            }
            return View();
        }
        [HttpPost]
        public ActionResult UpMyFile()
        {
            if (Request.InputStream != null)
            {
               byte[] mydata= new byte[Request.InputStream.Length];
               Request.InputStream.Read(mydata, 0, mydata.Length);
               FileStream fs = new FileStream(Server.MapPath("~/dstfile/1.JPG"), FileMode.Create, FileAccess.Write);
               fs.Write(mydata, 0, mydata.Length);
               fs.Close();
            }
            return null;
        }
        public ActionResult GetMyVal()
        {
            using (MemcachedClient mc = new MemcachedClient())
            {
                var myunderwear = mc.Get("u1") as MvcApplication1.Models.UnderWear;
                if (myunderwear != null)
                {
                    Response.Write(string.Format("您的内衣信息为：ID:{0},内衣名字：{1},生产日期：{2}", myunderwear.Uid, myunderwear.UderWearName, myunderwear.AddTime.ToString()));
                }
                else
                {
                    Response.Write("很抱歉，暂时没有找到！");
                }
                
            }
            return null;
        }
    }
}
