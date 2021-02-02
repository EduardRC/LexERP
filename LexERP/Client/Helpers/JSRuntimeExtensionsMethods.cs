using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexERP.Client.Helpers
{
    public static class JSRuntimeExtensionsMethods
    {
        public static async ValueTask InicializarTimerInactivo<T>(this IJSRuntime js,
            DotNetObjectReference<T> dotNetObjectReference) where T : class
        {
            await js.InvokeVoidAsync("timerInactivo", dotNetObjectReference);
        }

        public static async ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
            => await js.InvokeAsync<object>(
                "localStorage.setItem",
                key, content
                );

        public static async ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => await js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public static async ValueTask<object> RemoveLocalStorage(this IJSRuntime js, string key)
            => await js.InvokeAsync<object>(
                "localStorage.removeItem",
                key
                );

        public static async ValueTask<object> SetInSessionStorage(this IJSRuntime js, string key, string content)
            => await js.InvokeAsync<object>(
                "sessionStorage.setItem",
                key, content
                );

        public static async ValueTask<string> GetFromSessionStorage(this IJSRuntime js, string key)
            => await js.InvokeAsync<string>(
                "sessionStorage.getItem",
                key
                );

    }
}
