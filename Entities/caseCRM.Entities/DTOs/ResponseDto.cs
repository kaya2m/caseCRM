using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.Entities.DTOs
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public object Content { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public ErrorDto Error { get; set; }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<T> Success(List<T> dataList, int statusCode)
        {
            return new ResponseDto<T> { DataList = dataList, StatusCode = statusCode, IsSuccessful = true };
        }
        public static ResponseDto<T> Success(T data, string message, int statusCode)
        {
            return new ResponseDto<T> { Data = data, Message = message, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<T> Success(object content, string message, int statusCode)
        {
            return new ResponseDto<T> { Content = content, Message = message, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<T> Success(string message, int statusCode)
        {
            return new ResponseDto<T> { Data = default, Message = message, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<T> Success(string message)
        {
            return new ResponseDto<T> { Message = message, IsSuccessful = true };
        }

        public static ResponseDto<T> Fail(ErrorDto errorDto, int statusCode)
        {
            return new ResponseDto<T> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }

        public static ResponseDto<T> Fail(ErrorDto errorDto, string message, int statusCode)
        {
            return new ResponseDto<T> { Error = errorDto, Message = message, StatusCode = statusCode, IsSuccessful = false };
        }

        public static ResponseDto<T> Fail(string message, int statusCode, bool isShow)
        {
            return new ResponseDto<T> { Error = new ErrorDto(message, isShow), StatusCode = statusCode, IsSuccessful = false };
        }

        public static ResponseDto<T> Fail(string message)
        {
            return new ResponseDto<T> { Message = message, IsSuccessful = false };
        }
        public static ResponseDto<T> Fail(string message,int statusCode)
        {
            return new ResponseDto<T> { Message = message, IsSuccessful = false, StatusCode = statusCode };
        }
        public static ResponseDto<T> Fail(object content, string message, int statusCode)
        {
            return new ResponseDto<T> { Content = content, Message = message, StatusCode = statusCode, IsSuccessful = false };
        }

        public static ResponseDto<T> Fail(object content, string message)
        {
            return new ResponseDto<T> { Content = content, Message = message, IsSuccessful = false };
        }
    }
}
