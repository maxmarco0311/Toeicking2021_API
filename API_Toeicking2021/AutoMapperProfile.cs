using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021
{
    public class AutoMapperProfile : Profile
    {
        // 要寫在建構式內
        public AutoMapperProfile()
        {
            //使用CreateMap<sourceType, destinationType>();建立物件轉換關係
            CreateMap<Sentence, SentenceDto>();
            CreateMap<Vocabulary, VocabularyDto>();
            CreateMap<GA, GADto>();
            CreateMap<VA, VADto>();

            CreateMap<User, GetUserDto>();
            CreateMap<AddUserDto, User>();




        }
    }
}
