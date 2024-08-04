using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryManager.Application.Responses
{
    public class APIResponse<TData>
    {
        [JsonPropertyName("operation_type")]
        public string? OperationType { get; private set; }

        [JsonPropertyName("is_successfully")]
        public bool IsSuccessfully { get; private set; }

        [JsonPropertyName("code_response")]
        public int CodeReponse { get; private set; }

        [JsonPropertyName("message")]
        public string? Message { get; private set; }

        [JsonPropertyName("data_response")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData? DataResponse { get; private set; }

        [JsonPropertyName("data_response_list")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<TData?>? DataResponseList { get; private set; }

        public APIResponse(string? operationType,
            bool isSuccessfully,
            int codeReponse,
            string? message,
            TData? dataResponse,
            List<TData?>? dataResponseList)
        {
            OperationType = operationType;
            IsSuccessfully = isSuccessfully;
            CodeReponse = codeReponse;
            Message = message;
            DataResponse = dataResponse;
            DataResponseList = dataResponseList;
        }

        public APIResponse(string? operationType,
            bool isSuccessfully,
            int codeReponse,
            string? message)
        {
            OperationType = operationType;
            IsSuccessfully = isSuccessfully;
            CodeReponse = codeReponse;
            Message = message;
        }

        public APIResponse()
        {
        }
    }
}

