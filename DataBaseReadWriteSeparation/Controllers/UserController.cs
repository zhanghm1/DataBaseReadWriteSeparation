using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseReadWriteSeparation.Data;
using DataBaseReadWriteSeparation.Models;
using EFCore.DatabaseChoose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataBaseReadWriteSeparation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private EFRepository<Users> _repositoryUser;

        public UserController(ILogger<UserController> logger, EFRepository<Users> repositoryUser)
        {
            _logger = logger;
            _repositoryUser = repositoryUser;
        }

        [HttpGet]
        public async Task<Users> Get()
        {
            return await _repositoryUser.FirstOrDefaultAsync(a => a.Id > 0);
        }

        [HttpGet]
        [Route("list")]
        [DatabaseChoose(true)]
        public async Task<IEnumerable<Users>> GetList()
        {
            var list = await _repositoryUser.GetListAsync(a => a.Id > 0);

            return list;
        }

        [HttpPost]
        public async Task<bool> Post()
        {
            Users user = new Users()
            {
                Name = "张三",
                Sex = Guid.NewGuid().ToString()
            };

            return await _repositoryUser.AddAsync(user);
        }
    }
}