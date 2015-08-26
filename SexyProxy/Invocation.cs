﻿using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SexyProxy
{
    public abstract class Invocation
    {
        public object Proxy { get; }
        public InvocationHandler InvocationHandler { get; }
        public MethodInfo Method { get; }
        public object[] Arguments { get; }

        public abstract Task<object> Proceed();

        protected Invocation(object proxy, InvocationHandler invocationHandler, MethodInfo method, object[] arguments)
        {
            Proxy = proxy;
            InvocationHandler = invocationHandler;
            Method = method;
            Arguments = arguments;
        }
    }

    public class InvocationT<T> : Invocation
    {
        private Func<object[], T> implementation;

        public InvocationT(object proxy, InvocationHandler invocationHandler, MethodInfo method, object[] arguments, Func<object[], T> implementation) : base(proxy, invocationHandler, method, arguments)
        {
            this.implementation = implementation;
        }

        public override Task<object> Proceed()
        {
            return Task.FromResult<object>(implementation(Arguments));
        }
    }
}
