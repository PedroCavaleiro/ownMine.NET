using System.Text.Json;

namespace Extensions;

public static class HttpResponseMessageExtension {

    public static async Task<T> ReadJsonContentAsAsync<T>(this HttpContent      content,
                                                          JsonSerializerOptions options = null) =>
        JsonSerializer.Deserialize<T>(await content.ReadAsStringAsync(), options);

}