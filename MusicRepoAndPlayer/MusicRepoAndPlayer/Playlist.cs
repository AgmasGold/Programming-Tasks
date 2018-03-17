﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace MusicRepoAndPlayer
{
    public enum TrackOptions {
            Success = 1,
            Fail = 2,
            Overwrite = 3
        }

    class Music
    {
         
        public Dictionary<string,Playlist> Playlists { get; set; }

        public Music()
        {
            Playlists = new Dictionary<string, Playlist>();
        }

        public bool AddPlaylist (Playlist playlist, bool displayDialog = false)
        {
            bool doOverwrite = true;
            if (Playlists.ContainsKey(playlist.PlaylistName))
            {
                if (displayDialog)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to overwrite the " + playlist.PlaylistName + " playlist? This will remove all the tracks associated with it!", "A playlist with this name already exists!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    doOverwrite = result == DialogResult.OK ? true : false;
                }
            }

            if (doOverwrite)
            {
                Playlists[playlist.PlaylistName] = playlist;
            }
            return doOverwrite;
        }

        public TrackOptions AddTrack(Track track, string playlistName, bool displayDialog = false)
        {
         
            bool writeTrack = true;
            bool dialogPassed = false;
            
            if (Playlists[playlistName].Tracks.ContainsKey(track.TrackName))
            {

                if (displayDialog)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to overwrite " + track.TrackName + " in the " + playlistName + " playlist?", "This track already exists!", MessageBoxButtons.OKCancel);
                    writeTrack = result == DialogResult.OK;
                    dialogPassed = result == DialogResult.OK;
                }

            }
            if (writeTrack)
            {
                Playlists[playlistName].Tracks[track.TrackName] = track;
                return dialogPassed ? TrackOptions.Overwrite : TrackOptions.Success;
            }
            return TrackOptions.Fail;
        }

        public Track FindTrack(string trackName, string playlistName)
        {
            if (Playlists.ContainsKey(playlistName))
            {
                return Playlists[playlistName].Tracks[trackName];
            }
            return null;
        }

        public Track DeleteTrack(string trackName, string playlistName, bool displayDialog = false)
        {
            bool doDelete = true;
            Track deletedTrack = null;
            if (Playlists.ContainsKey(playlistName))
            {
                if (Playlists[playlistName].Tracks.ContainsKey(trackName))
                {
                    if (displayDialog)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete " + trackName + "?", "Delete Track", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        doDelete = result == DialogResult.OK ? true : false;
                    }
                    if (doDelete)
                    {
                        deletedTrack = Playlists[playlistName].Tracks[trackName];
                       Playlists[playlistName].Tracks.Remove("trackName");
                    }
                    return deletedTrack;
                }
            }
            return null;
        }

        public void DeletePlaylist(string playlistName, bool displayDialog = false)
        {
            bool doDelete = true;
            if (Playlists.ContainsKey(playlistName))
            {

                if (displayDialog)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete " + playlistName + "?", "Delete Playlist", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    doDelete = result == DialogResult.OK ? true : false;
                }
                if (doDelete)
                {

                    Playlists.Remove(playlistName);
                }
          

            }
        }

        public List<Track> GetPlaylist(string playlistName)
        {
            Playlist playlist = Playlists[playlistName];
            List<Track> tracks = new List<Track>();
            foreach (KeyValuePair<string, Track> item in playlist.Tracks)
            {
                tracks.Add(item.Value);
            }
            return tracks;
        }

        

    }

    class Playlist
    {
        public string PlaylistName { get; set; }
        public Dictionary<string, Track> Tracks { get; set; }

        public Playlist()
        {

        }

        public Playlist(string playlistName)
        {
            PlaylistName = playlistName;
            Tracks = new Dictionary<string, Track>();
        }

    }

    class Track
    {
        public string TrackName { get; set; }
        public string Link { get; set; }
        public string SearchString { get; set; }
        public Track()
        {

        }

        public Track(string trackName, string link, string searchString)
        {
            TrackName = trackName;
            Link = link;
            SearchString = searchString;
        }
    }
}