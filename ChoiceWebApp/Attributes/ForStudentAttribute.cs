using ChoiceWebApp.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ChoiceWebApp.Attributes
{
    public class ForStudentAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
            new ForStudentFilter();
    }
}