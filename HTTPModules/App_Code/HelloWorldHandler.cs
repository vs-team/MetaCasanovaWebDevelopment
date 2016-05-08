using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public class DataHandler : IHttpHandler
{
  public DataHandler() { }

  public void ProcessRequest(HttpContext context)
  {
    HttpRequest Request = context.Request;
    HttpResponse Response = context.Response;

    if (IsAjaxRequest)
    {
      string method = Request.QueryString["MethodName"].ToString();
      Response.ContentType = "text/json";
      switch (method)
      {
        case "GetData":
          Response.Write(GetData());
          break;
        default:
          Response.Write("Cannot find method" + method);
          break;

      }
    }
    else {
      Response.Write("Error: non-ajax requests to data handler are not allowed.");
    }
  }

  protected string GetData()
  {
    return (@"{""FirstName"":""Java"", ""LastName"":""Script"", 
               ""Blog"":""sucks.balls.com""}");
  }

  public bool IsReusable
  {
    get { return false; }
  }

  public bool IsAjaxRequest
  {
    get
    {
      return HttpContext.Current.Request.Headers["X-Requested-With"] != null && HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }
  }

}
