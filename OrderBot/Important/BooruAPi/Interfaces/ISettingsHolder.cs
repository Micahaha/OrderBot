namespace BooruAPI.Core
{
    /// <summary> Represents a settings store</summary>
    public interface ISettingsHolder
    {
        /// <summary> Returns the value of the setting.</summary>
        /// <typeparam name="T"> The type of the setting.</typeparam>
        /// <param name="settingName"> The name of the setting to fetch.</param>
        /// <returns> The value of the setting.</returns>
        T GetSettingOrDefault<T>(string settingName);

        /// <summary> Tries to get the value of a setting</summary>
        /// <typeparam name="T"> The type of the setting.</typeparam>
        /// <param name="settingName"> The name of the setting to fetch.</param>
        /// <param name="settingValue"> The value of the setting.</param>
        /// <returns> True if the setting has been found and is of type <typeparamref name="T"/>.
        /// False if not found or a cast to <typeparamref name="T"/> cannot be made.</returns>
        bool TryGetSetting<T>(string settingName, out T settingValue);

        /// <summary> Sets the value of a setting.</summary>
        /// <typeparam name="T"> The type of the setting.</typeparam>
        /// <param name="settingName"> The name of the setting to set.</param>
        /// <param name="settingValue"> The value of the setting.</param>
        void SetSetting<T>(string settingName, T settingValue);

        /// <summary> Removes a setting.</summary>
        /// <param name="settingName"> The name of the setting to remove.</param>
        /// <returns> True if the setting has been removed.</returns>
        bool RemoveSetting(string settingName);
    }
}
