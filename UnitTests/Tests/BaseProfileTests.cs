using System;
using AutoMapper;
using MapperExtensions.Models;
using NUnit.Framework;

namespace Tests
{
    public abstract class BaseProfileTests<TProfile, TAggregateEntity> where TProfile : Profile, new()
        where TAggregateEntity : class
    {
        protected TestDbContext context { get; private set; }


        public BaseProfileTests()
        {
            Mapper.Initialize(x => { x.AddProfile<TProfile>(); });
        }
        [SetUp]
        protected void Setup()
        {
            context = new TestDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        
        protected abstract TAggregateEntity MockData();
       
    }
}