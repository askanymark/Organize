using System;
using BlazorExample.Components.DependencyInjection.Interfaces;

namespace BlazorExample.Components.DependencyInjection.Implementations
{
    public class ConstructorInjectionTwo : IConstructorInjectionTwo
    {
        public void MethodOfTwo()
        {
            Console.WriteLine("Two");
        }
    }
}