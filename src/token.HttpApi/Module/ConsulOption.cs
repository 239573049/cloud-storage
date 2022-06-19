namespace token.HttpApi.Module;

public class ConsulOption
{
    public const string Name = "Consul";
    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }
    
    /// <summary>
    /// 当前服务ip
    /// </summary>
    public string ServiceIP{ get; set; }
    
    /// <summary>
    /// 当前服务端口
    /// </summary>
    public  int ServicePort{ get; set; }
    
    /// <summary>
    /// 健康检查地址
    /// </summary>
    public string ServiceHealthCheck { get; set; }
    
    /// <summary>
    /// Consul地址
    /// </summary>
    public string Address { get; set; }
}