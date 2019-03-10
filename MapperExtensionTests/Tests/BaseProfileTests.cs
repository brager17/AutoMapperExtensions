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

        [SetUp]
        protected void Setup()
        {
            context = new TestDbContext();
            context.Database.EnsureCreated();
            context.Add(MockData());
            context.SaveChanges();
             Mapper.Initialize(x => { x.AddProfile<TProfile>(); });
        }

        protected abstract TAggregateEntity MockData();

        [TearDown]
        protected void TearDown()
        {
            context.Database.EnsureDeleted();
        }
    }
}