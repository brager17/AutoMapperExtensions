using System;
using MapperExtensions.Models;
using Tests.Models.StudioModels;

namespace Tests
{
    public class AdvancedFromStudioProfileTests:BaseProfileTests<StudioProfile,Studio>
    {
        protected override Studio MockData()
        {
            throw new NotImplementedException();
        }
        
    }
}