﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseChoose;
using DataBaseReadWriteSeparation.Data;
using DataBaseReadWriteSeparation.Models;
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
        public async Task<IEnumerable<Users>>  Get()
        {
            return await _repositoryUser.GetListAsync(a => a.Id > 0);
        }
        [HttpGet]
        [Route("add")]
        [DatabaseChoose(true)]
        public async Task<IEnumerable<Users>> post()
        {
            Users user = new Users() {
            Name="张三",
            Sex= Guid.NewGuid().ToString()
            };
            await _repositoryUser.AddAsync(user);


           var list = await _repositoryUser.GetListAsync(a => a.Id > 0);

           return list;
        }
    }
}
