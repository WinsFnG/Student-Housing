window.addEventListener('DOMContentLoaded', () => {
    const target = document.getElementById('notificationsComponent');

    if (target) {
        const notifContainer = document.createElement('div');
        notifContainer.classList.add('notif-container');
        notifContainer.innerHTML = `
            <div class="notif" id="notifBtn">
                <span class="material-icons-outlined">notifications</span>
            </div>
            <div class="notifications-popup" id="notificationsPopup">
                <div class="popup-header">
                    <h4>Notifications</h4>
                    <span class="popup-status">Empty</span>
                </div>
                <div class="popup-content">
                    <div class="empty-text">
                        <span class="material-icons-outlined" style="font-size:32px; color:#ccc;">notifications_off</span>
                        <p>No notifications yet</p>
                    </div>
                </div>
            </div>
        `;

        target.appendChild(notifContainer);

        const notifBtn = document.getElementById('notifBtn');
        const popup = document.getElementById('notificationsPopup');

        notifBtn.addEventListener('click', (e) => {
            e.stopPropagation();
            const isVisible = popup.style.display === 'block';
            popup.style.display = isVisible ? 'none' : 'block';
        });

        document.addEventListener('click', () => {
            if (popup) popup.style.display = 'none';
        });

        popup.addEventListener('click', (e) => {
            e.stopPropagation();
        });

        fetchNotifications();
    }

    async function fetchNotifications() {
        const popupContent = document.querySelector('.popup-content');
        const statusLabel = document.querySelector('.popup-status');

        try {
            const response = await fetch('api/notifications'); 
            const data = await response.json(); 

            if (data && data.length > 0) {
                statusLabel.textContent = data.length;
                statusLabel.style.background = "#c62828";
                statusLabel.style.color = "white";

                popupContent.innerHTML = `
                    <div style="width: 100%; display: flex; flex-direction: column;">
                        ${data.map(n => `
                            <div style="padding: 12px; border-bottom: 1px solid #eee; text-align: left;">
                                <p style="font-size: 13px; margin: 0; color: #333;">${n.message}</p>
                                <small style="color: #888;">${n.date}</small>
                            </div>
                        `).join('')}
                    </div>
                `;
            }
        } catch (err) {
            console.warn("Backend link 'api/notifications' not found yet.");
        }
    }
});


const styleNotif = document.createElement('style');
styleNotif.textContent = `
.notif-container { position: relative; z-index: 2000; }
.notif {
    font-size: 24px;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 40px;
    width: 40px;
    color: white;
    cursor: pointer;
    transition: 0.3s;
}
.notif:hover { transform: scale(1.1); color: #81d4fa; }
.notifications-popup {
    position: absolute;
    top: 50px;
    right: -10px;
    width: 280px;
    background: white;
    border-radius: 12px;
    box-shadow: 0 8px 25px rgba(0,0,0,0.2);
    padding: 15px;
    display: none;
}
.popup-header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 10px; border-bottom: 1px solid #eee; padding-bottom: 8px; }
.popup-header h4 { font-size: 15px; color: #1E3A5F; margin:0; font-family: sans-serif; }
.popup-status { font-size: 10px; padding: 2px 6px; border-radius: 10px; background: #eee; color: #777; font-family: sans-serif; }
.popup-content { min-height: 80px; max-height: 350px; overflow-y: auto; font-family: sans-serif; }
.empty-text { text-align: center; padding: 20px; color: #bbb; font-size: 13px; }
`;
document.head.appendChild(styleNotif);
