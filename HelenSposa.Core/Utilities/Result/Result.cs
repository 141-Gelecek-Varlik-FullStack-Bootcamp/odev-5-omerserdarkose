using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelenSposa.Core.Utilities.Result
{
    /// <summary>
    /// data donusu olmayan sadece isleme dair sonucu bildirirken kullanilacak result yapisi
    /// sadece success durumu donebilir veya hem success durumu hemde mesaj donebilir
    /// </summary>
    public class Result : IResult
    {
        [JsonConstructor]
        public Result(bool success, string message):this (success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
