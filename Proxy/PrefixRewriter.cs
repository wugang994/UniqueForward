namespace Reader.Proxy
{
    public class PrefixRewriter : IUrlRewriter
    {
        //private readonly PathString _prefix; //前缀值
        private readonly string _newHost; //转发的地址
        private IConfiguration _configuration;

        public PrefixRewriter(IConfiguration configuration)
        {
            _configuration = configuration;
            _newHost = _configuration["NewHost"];
        }

        public Task<Uri> RewriteUri(HttpContext context)
        {
            var newUri = context.Request.Path.Value + context.Request.QueryString;
            var targetUri = new Uri(_newHost + newUri);
            return Task.FromResult(targetUri);
        }
    }
}
