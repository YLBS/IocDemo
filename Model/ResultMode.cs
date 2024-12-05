using Model.enums;

namespace Model
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class ResultMode<T>
    {
        public ResponseCode Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ResultMode(ResponseCode code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResultMode<T> Success(T data = default, string message = "Success")
        {
            return new ResultMode<T>(ResponseCode.Success, message, data);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResultMode<T> Failed(string message = "Failed")
        {
            return new ResultMode<T>(ResponseCode.Failed, message, default);
        }
        /// <summary>
        /// 找不着
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResultMode<T> NotFound(string message = "Not Found")
        {
            return new ResultMode<T>(ResponseCode.NotFound, message, default);
        }
    }
}
