using System.Threading.Tasks;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.ConfigProvider
{
    public interface IConfigProvider
    {
        Task<T> GetConfigAsync<T>(string key = "default") where T : ScriptableObject;
    }
}
