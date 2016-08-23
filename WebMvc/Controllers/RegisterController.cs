using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class RegisterController : Controller
    {

        //短信验证码接口的测试数据（天下畅通平台给参数）  
        public static String url = "http://xtx.telhk.cn:8080/sms.aspx";
        public static String userid = "5251";
        public static String account = "txt03";
        public static String password = "12345678";

        public ActionResult Index()
        {
            return View();
        }


        #region GetCode()-获取验证码
        /// <summary>
        /// 返回json到界面
        /// </summary>
        /// <returns>string</returns>
        public ActionResult GetCode()
        {
            try
            {
                bool result;
                //接收前台传过来的参数。短信验证码和手机号码
                string code = Request["Code"];
                string phoNum = Request["phoNum"];

                // 短信验证码存入session(session的默认失效时间30分钟) 
                //也可存入Memcached缓存
                Session.Add("code", code);

                // 短信内容+随机生成的6位短信验证码    
                String content = "【欢迎注册】 您的注册验证码为:" + code + "，如非本人操作请忽略。有疑问请联系我们：http://blog.csdn.net/u010028869";

                // 单个手机号发送短信
                //if (!SendMessage(content, phoNum, url, userid, password, account))
                //{
                //    result = false;// 失败    
                //}
                //else
                //{
                //    result = true;// 成功    
                //}
                //测试
                result = true;// 成功    
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 短信发送方法
        /// </summary>
        /// <param name="content">短信内容</param>
        /// <param name="phoNum">手机号码</param>
        /// <param name="url">请求地址</param>
        /// <param name="userid">企业id</param>
        /// <param name="password">密码</param>
        /// <param name="account">用户帐号</param>
        /// <returns>bool 是否发送成功</returns>
        public bool SendMessage(string content, string phoNum, string url, string userid, string password, string account)
        {
            try
            {
                Encoding myEncoding = Encoding.GetEncoding("UTF-8");
                //按照平台给定格式，组装发送参数 包括用户id，密码，账户，短信内容，账户等等信息
                string param = "action=send&userid=" + userid + "&account=" + HttpUtility.UrlEncode(account, myEncoding) + "&password=" + HttpUtility.UrlEncode(password, myEncoding) + "&mobile=" + phoNum + "&content=" + HttpUtility.UrlEncode(content, myEncoding) + "&sendTime=";

                //发送请求
                byte[] postBytes = Encoding.ASCII.GetBytes(param);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                req.ContentLength = postBytes.Length;

                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                }

                
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                //获取返回的结果
                using (WebResponse wr = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8);
                    System.IO.StreamReader xmlStreamReader = sr;
                    //加载XML文档
                    xmlDoc.Load(xmlStreamReader);
                }
                //解析XML文档，进行相应判断
                if (xmlDoc == null)
                {
                    return false;
                }
                else
                {
                    String message = xmlDoc.GetElementsByTagName("message").Item(0).InnerText.ToString();
                    if (message == "ok")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region CheckCode()-检查验证码是否正确
        public ActionResult CheckCode()
        {
            bool result = false;
            //用户输入的验证码
            string checkCode = Request["CheckCode"].Trim();
            //取出存在session中的验证码
            string code = Session["code"].ToString();
            try
            {
                //验证是否一致
                if (checkCode != code)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception("短信验证失败", e);
            }
        }
        #endregion
    }
}
