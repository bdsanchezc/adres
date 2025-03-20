
namespace AdquisicionesAPI.Models
{
  public class Response<T>
  {
    public string Status { get; set; }
    public T Data { get; set; }

    public Response(string status, T data)
    {
      Status = status;
      Data = data;
    }
  }
}

