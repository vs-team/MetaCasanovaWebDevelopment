using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

public class AuthenticationResult
{
  public bool Success;
  public string Username;
}

public class LoginRequest
{
  public string Method;
  public string Email;
  public string Password;
}

public class LoginHandler : IHttpHandler
{
  public LoginHandler() { }

  static JavaScriptSerializer json = new JavaScriptSerializer();

  public void ProcessRequest(HttpContext context)
  {
    HttpRequest request = context.Request;
    HttpResponse response = context.Response;

    if (IsAjaxRequest)
    {
      var loginData = json.Deserialize<LoginRequest>(request.Form[0]);

      response.ContentType = "text/json";
      switch (loginData.Method)
      {
        case "Login":
          response.Write(json.Serialize(GetData(loginData)));
          break;
        default:
          response.Write("Cannot find method" + loginData.Method);
          break;

      }
    }
    else {
      response.Write("Error: non-ajax requests to data handler are not allowed.");
    }
  }

  protected AuthenticationResult GetData(LoginRequest loginData)
  {
    return new AuthenticationResult() { Username = loginData.Email, Success = true };
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
