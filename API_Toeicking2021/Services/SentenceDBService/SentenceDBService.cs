using API_Toeicking2021.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Services.SentenceDBService
{
    public class SentenceDBService : ISentenceDBService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SentenceDBService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }





    }
}
