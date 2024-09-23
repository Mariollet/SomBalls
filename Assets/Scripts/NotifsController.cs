using UnityEngine;
using Unity.Notifications.Android;

public class NotifsController : MonoBehaviour
{
    private static NotifsController instance;
    public AndroidNotificationChannel defaultNotificationChannel;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        defaultNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);

        SendNotif();
    }

    public void SendNotif()
    {
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Tu n'as toujours pas dépassé ton HighScore ?!",
            Text = "Appuie ici pour continuer",
            SmallIcon = "icon_1",
            FireTime = System.DateTime.Now.AddHours(2),
        };
        AndroidNotificationCenter.SendNotification(notification, "default_channel");
        AndroidNotification notification_0 = new AndroidNotification()
        {
            Title = "Prêt à remettre ça ?",
            Text = "Appuie ici pour continuer",
            SmallIcon = "icon_1",
            FireTime = System.DateTime.Now.AddHours(24),
        };
        AndroidNotificationCenter.SendNotification(notification_0, "default_channel");
    }
}