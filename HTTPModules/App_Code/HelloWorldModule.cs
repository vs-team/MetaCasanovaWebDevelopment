using System;
using System.Web;
public class HelloWorldModule : IHttpModule
{
  public HelloWorldModule()
  {
  }

  public String ModuleName
  {
    get { return "HelloWorldModule"; }
  }

  // In the Init function, register for HttpApplication 
  // events by adding your handlers.
  public void Init(HttpApplication application)
  {
    application.BeginRequest +=
        (new EventHandler(this.Application_BeginRequest));
    application.EndRequest +=
        (new EventHandler(this.Application_EndRequest));
  }

  private void Application_BeginRequest(Object source,
       EventArgs e)
  {
    // Create HttpApplication and HttpContext objects to access
    // request and response properties.
    HttpApplication application = (HttpApplication)source;
    HttpContext context = application.Context;
    string filePath = context.Request.FilePath;
    string fileExtension =
        VirtualPathUtility.GetExtension(filePath);
    if (fileExtension.Equals(".html"))
    {
      //context.Response.Write(@"<div><font color=red>Login form goes here</font></div>");
    }
  }

  private void Application_EndRequest(Object source, EventArgs e)
  {
    HttpApplication application = (HttpApplication)source;
    HttpContext context = application.Context;
    string filePath = context.Request.FilePath;
    string fileExtension =
        VirtualPathUtility.GetExtension(filePath);
    if (fileExtension.Equals(".html"))
    {
      //context.Response.Write(
      //@"<footer>
      //  <p>Made by: Giuseppe Maggiore</p>
      //  <p>Contact information: <a href=""mailto: someone@example.com"">
      //  someone@example.com </a>.</p>
      //</footer>");
    }
  }

  public void Dispose() { }
}
