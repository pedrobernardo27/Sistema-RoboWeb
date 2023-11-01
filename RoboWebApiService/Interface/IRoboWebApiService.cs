using RoboWebApiService.Model;

namespace RoboWebApiService.Interface
{
    public interface IRoboWebApiService
    {
        ValueTask<IEnumerable<UdemyModel>> BuscarUdemyGoogle();
    }
}
