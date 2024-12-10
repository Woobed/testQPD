using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testQPD.DaData.Interfaces;
using testQPD.Models;

namespace testQPD.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class AddressController : ControllerBase
    {
        private readonly IDaDataClient _daDataClient;
        private readonly IMapper _mapper;

        public AddressController(IDaDataClient _daDataClient, IMapper _mapper)
        {
            this._daDataClient = _daDataClient;
            this._mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AddressModel>> GetAddress([FromQuery] string address)
        {
            var response = await _daDataClient.CleanAddressAsync(address);
            var model = _mapper.Map<AddressModel>(response);
            return Ok(model);
        }

    }

}
