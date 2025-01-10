<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
=======
﻿using Microsoft.AspNetCore.Http; // Provides access to the session interface
using Newtonsoft.Json; // For JSON serialization and deserialization

// Static class for extending ISession functionality
public static class SessionExtensions
{
    // Sets a strongly-typed object in the session
    public static void Set<T>(this ISession session, string key, T value)
    {
        // Serialize the object to a JSON string and store it in the session
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    // Gets a strongly-typed object from the session
    public static T Get<T>(this ISession session, string key)
    {
        // Retrieve the JSON string from the session
        var value = session.GetString(key);

        // If the value is null, return the default value for the type T; otherwise, deserialize the JSON string
>>>>>>> 69e884f (Initial project upload)
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}
