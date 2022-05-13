namespace Reader.Proxy
{
    public interface IUrlRewriter
    {
        Task<Uri> RewriteUri(HttpContext context);
    }
}
