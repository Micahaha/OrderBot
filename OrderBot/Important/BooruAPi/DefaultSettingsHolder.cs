using System.Collections.Generic;

namespace BooruAPI.Core
{
    /// <summary> An implementation of <see cref="ISettingsHolder"/> for storing settings.</summary>
    public sealed class DefaultSettingsHolder : ISettingsHolder
    {
        /// <summary> The backend cache of settings.</summary>
        private readonly Dictionary<string, object> _settings = new Dictionary<string, object>();

        /// <summary> Returns the value of the setting.</summary>
        /// <typeparam name="T"> The type of the setting.</typeparam>
        /// <param name="settingName"> The name of the setting to fetch.</param>
        /// <returns> The value of the setting.</returns>
        public T GetSettingOrDefault<T>(string settingName) =>
            (_settings.TryGetValue(settingName, out var v) && v is T value) ? value : default;

        /// <summary> Tries to get the value of a setting</summary>
        /// <typeparam name="T"> The type of the setting.</typeparam>
        /// <param name="settingName"> The name of the setting to fetch.</param>
        /// <param name="settingValue"> The value of the setting.</param>
        /// <returns> True if the setting has been found and is of type <typeparamref name="T"/>.
        /// False if not found or a cast to <typeparamref name="T"/> cannot be made.</returns>
        public void SetSetting<T>(string settingName, T settingValue) =>
            _settings[settingName] = settingValue;

        /// <summary> Sets the value of a setting.</summary>
        /// <typeparam name="T"> The type of the setting.</typeparam>
        /// <param name="settingName"> The name of the setting to set.</param>
        /// <param name="settingValue"> The value of the setting.</param>
        public bool TryGetSetting<T>(string settingName, out T settingValue)
        {
            if (_settings.TryGetValue(settingName, out var v) && v is T value)
            {
                settingValue = value;
                return true;
            }
            else
            {
                settingValue = default;
                return false;
            }
        }

        /// <summary> Removes a setting.</summary>
        /// <param name="settingName"> The name of the setting to remove.</param>
        /// <returns> True if the setting has been removed.</returns>
        public bool RemoveSetting(string settingName) =>
            _settings.Remove(settingName);
    }
}
