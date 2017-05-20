using TwitterForUnity;

// Twitter for Unity の Setting からの分離
public static class TwitterKeysExtentions {
    public static Client CreateClient(this TwitterKeys keys)
    {
        var oauth = new Oauth(
            keys.ConsumerKey,
            keys.ConsumerSecret,
            keys.AccessToken,
            keys.AccessTokenSecret);
        return new Client(oauth);
    }
}
