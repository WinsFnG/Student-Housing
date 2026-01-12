const target = document.getElementById('notificationsComponent');

// Pojistka: Skript se spustí jen pokud na stránce existuje cíl
if (target) {
    const notifContainer = document.createElement('div');
    notifContainer.classList.add('notif-container');
    notifContainer.innerHTML = `
        <div class="notif" id="notifBtn">
            <i class="fa-regular fa-bell"></i>
        </div>
        <div class="notifications-popup" id="notificationsPopup">
            <div class="popup-header">
                <h4>Notifications</h4>
                <span class="popup-status">Empty</span>
            </div>
            <div class="popup-content">
                <div class="empty-text">
                    <i class="fa-regular fa-bell-slash"></i>
                    <p>No notifications yet</p>
                </div>
            </div>
        </div>
    `;

    target.appendChild(notifContainer);

    const notifBtn = document.getElementById('notifBtn');
    const popup = document.getElementById('notificationsPopup');

    // Otevírání/Zavírání popupu
    notifBtn.addEventListener('click', (e) => {
        e.stopPropagation();
        popup.style.display = popup.style.display === 'block' ? 'none' : 'block';
    });

    document.addEventListener('click', () => {
        popup.style.display = 'none';
    });

    // Zamezení zavření popupu při kliknutí dovnitř něj
    popup.addEventListener('click', (e) => {
        e.stopPropagation();
    });

    // --- LOGIKA PRO KOLEGU (BACKEND) ---
    async function fetchNotifications() {
        const popupContent = document.querySelector('.popup-content');
        const statusLabel = document.querySelector('.popup-status');

        try {
            // Tady kolega změní URL na svůj backend
            const response = await fetch('api/notifications'); 
            const data = await response.json(); 

            if (data && data.length > 0) {
                statusLabel.textContent = data.length;
                statusLabel.style.background = "#39b5ac";
                statusLabel.style.color = "white";

                popupContent.innerHTML = `
                    <div style="width: 100%; display: flex; flex-direction: column;">
                        ${data.map(n => `
                            <div style="padding: 10px; border-bottom: 1px solid #eee;">
                                <p style="font-size: 13px; margin: 0; color: #333;">${n.message}</p>
                                <small style="color: #888;">${n.date}</small>
                            </div>
                        `).join('')}
                    </div>
                `;
            }
        } catch (err) {
            console.log("Backend zatím neodpovídá, používám výchozí stav.");
        }
    }

    // Načteme data hned po načtení stránky
    fetchNotifications();
}

// STYLY (zůstávají stejné)
const style = document.createElement('style');
style.textContent = `
.notif-container { position: relative; margin-top: -20px; }
.notif {
    font-size: 26px;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 40px;
    width: 40px;
    border: 2px solid #5a5a5a;
    border-radius: 50%;
    cursor: pointer;
    transition: background 0.3s, transform 0.2s;
}
.notif:hover {
    background: #4FB8A5;
    box-shadow: 0 4px 10px rgba(0,0,0,0.2);
    transform: scale(1.05);
}
.notif:hover i { color: white; }
.notifications-popup {
    position: absolute;
    top: 50px;
    right: 0;
    width: 300px;
    background: white;
    border-radius: 12px;
    box-shadow: 0 10px 25px rgba(0,0,0,0.15);
    padding: 15px;
    display: none;
    z-index: 999;
}
.popup-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 10px;
}
.popup-header h4 { font-size: 16px; color: #1E3A5F; font-weight: 600; }
.popup-status {
    font-size: 12px;
    padding: 4px 8px;
    border-radius: 12px;
    background: #eee;
    color: #777;
}
.popup-content {
    min-height: 120px;
    display: flex;
    justify-content: center;
    align-items: center;
    max-height: 400px;
    overflow-y: auto;
}
.empty-text {
    text-align: center;
    font-size: 14px;
    color: #777;
}
.empty-text i {
    font-size: 28px;
    margin-bottom: 8px;
    color: #bbb;
}
`;
document.head.appendChild(style);
