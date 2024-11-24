using System;
using System.Collections.Generic;

namespace Game.Framework.EventSystem
{
    /// <summary>
    /// 事件中心，收集并管理事件
    /// </summary>
    public static class EventCenter
    {
        public static readonly EmptyArgs EmptyArgs = new();

        /// <summary>
        /// 事件实例容器
        /// </summary>
        private static readonly Dictionary<Type, IEvent> Events = new();

        /// <summary>
        /// 获取事件实例
        /// <para>若缓存中没有，则自动创建并加入缓存</para>
        /// </summary>
        /// <typeparam name="T">事件具体类型</typeparam>
        /// <returns>事件实例</returns>
        public static T Get<T>() where T : IEvent, new()
        {
            var type = typeof(T);
            if (Events.TryGetValue(type, out var gameEvent))
            {
                return (T)gameEvent;
            }

            gameEvent = new T();
            Events[type] = gameEvent;

            return (T)gameEvent;
        }

        /// <summary>
        /// 为无参数事件添加监听
        /// </summary>
        /// <param name="action">监听函数</param>
        /// <typeparam name="T">事件具体类型</typeparam>
        public static void AddListener<T>(Action action) where T : Event, new()
        {
            Event e = Get<T>();
            e.Triggered += _ => action?.Invoke();
        }

        /// <summary>
        /// 为有参数事件添加监听
        /// </summary>
        /// <param name="action">监听函数</param>
        /// <typeparam name="TEvent">事件具体类型</typeparam>
        /// <typeparam name="TEventArgs">事件所需参数类型</typeparam>
        public static void AddListener<TEvent, TEventArgs>(Action<TEventArgs> action)
            where TEvent : Event<TEventArgs>, new()
        {
            var e = Get<TEvent>();
            e.Triggered += action;
        }

        /// <summary>
        /// 触发无参数事件
        /// </summary>
        /// <typeparam name="T">事件具体类型</typeparam>
        public static void Trigger<T>() where T : Event, new()
        {
            Get<T>().Trigger(EmptyArgs);
        }

        /// <summary>
        /// 触发有参数事件
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <typeparam name="TEvent">事件具体类型</typeparam>
        /// <typeparam name="TEventArgs">事件所需参数类型</typeparam>
        public static void Trigger<TEvent, TEventArgs>(TEventArgs args) where TEvent : Event<TEventArgs>, new()
        {
            Get<TEvent>().Trigger(args);
        }
    }
}