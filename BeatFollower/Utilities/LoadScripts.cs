﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;


namespace BeatFollower.Utilities
{
    class LoadScripts
    {
        static public Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        static public IEnumerator LoadSpriteCoroutine(string spritePath, Action<Texture2D> done)
        {

            //if (_cachedTextures.ContainsKey(spritePath))
            //{

            //    done?.Invoke(_cachedTextures[spritePath]);
            //    yield break;
            //}

            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(spritePath))
            {
                www.SetRequestHeader("User-Agent", "BeatFollower/1.0.0");
                yield return www.SendWebRequest();

                if (www.isHttpError || www.isNetworkError)
                {
                    Logger.log.Warn("Unable to download sprite! Exception: " + www.error);
                }
                else
                {
                    Texture2D tex = DownloadHandlerTexture.GetContent(www);
                    //_cachedTextures.Add(spritePath, tex);
                    done?.Invoke(tex);
                }
            }
        }

    }
}
