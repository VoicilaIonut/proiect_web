const API_URL = "http://localhost:5275/api";

const apiService = {
    async login(obj) {
        const url = `${API_URL}/users/login`;

        try {
            const response = await fetch(url, {
                method: "POST",
                body: JSON.stringify(obj),
                headers: {
                    "Content-Type": "application/json",
                },
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            if (response.status === 200) {
                const json = await response.json();
                localStorage.setItem("accessToken", json["accessToken"]);
            }

            return response;
        } catch (e) {
            console.error('An error occurred during the login process', e);
            return false;
        }
    },

    async register(obj) {
        try {
            const response = await fetch(`${API_URL}/users/register`, {
                method: "POST",
                body: JSON.stringify(obj),
                headers: {
                    "Content-Type": "application/json",
                },
            });
            return await response.json();
        } catch (e) {
            return false;
        }
    },

    async fetchGame({ params }) {
        const accessToken = localStorage.getItem("accessToken");
        const response = await fetch(`${API_URL}/games/${params.productId}`, {
            headers: { Authorization: `Bearer ${accessToken}` },
        });

        if (response.status === 404) {
            console.log("Game not found");
            return {};
        }

        return response.json();
    },

    async sendMapToApi(productId, shipsPlaced) {
        const accessToken = localStorage["accessToken"];
        try {
            const response = await fetch(`${API_URL}/games/${productId}`, {
                method: "PATCH",
                headers: {
                    Authorization: `Bearer ${accessToken}`,
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(shipsPlaced),
            });

            if (!response.ok) {
                return { success: false, message: await response.text() + ` HTTP error! status: ${response.status}` };
            }

            const contentType = response.headers.get("content-type");
            if (contentType && contentType.indexOf("application/json") !== -1) {
                return { success: true, message: await response.json() };
            } else {
                return { success: true, message: await response.text() };
            }
        } catch (error) {
            console.error(error);
            return { success: false, message: error.message };
        }
    },

    async joinGame(productId) {
        const accessToken = localStorage["accessToken"];
        const response = await fetch(`${API_URL}/games/join/${productId}`, {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${accessToken}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ UserId: localStorage.getItem('id') }),
        });

        if (!response.ok) {
            return { success: false, message: `HTTP error! status: ${response.status}` };
        }

      
        return { success: true, data: await response.json() };
    },

    async sendAttack(productId, indexesToApi) {
        const accessToken = localStorage["accessToken"];
        try {
            const response = await fetch(`${API_URL}/games/${productId}/move`, {
                method: "POST",
                headers: {
                    Authorization: `Bearer ${accessToken}`,
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(indexesToApi),
            });

            if (!response.ok) {
                return { success: false, message: await response.text() + ` HTTP error! status: ${response.status}` };
            }

            const json = await response.json();
            return { success: true, data: json };
        } catch (error) {
            console.error(error);
            return { success: false, message: error.message };
        }
    },

    async createGame() {
        const accessToken = localStorage["accessToken"];
        try {
            const response = await fetch(`${API_URL}/games/`, {
                method: "POST",
                headers: {
                    Authorization: `Bearer ${accessToken}`,
                },
            });

            if (!response.ok) {
                return { success: false, message: await response.text() + ` HTTP error! status: ${response.status}` };
            }

            const json = await response.json();
            return { success: true, message:"ok", data: json };
        } catch (error) {
            console.error(error);
            return { success: false, message: error.message };
        }
    },
    async getUserDetails() {
        const accessToken = localStorage["accessToken"];
        try {
            const response = await fetch(`${API_URL}/users/details/me`, {
                headers: { Authorization: `Bearer ${accessToken}` },
            });

            if (!response.ok) {
                return { success: false, message: await response.text() + ` HTTP error! status: ${response.status}` };
            }

            const json = await response.json();
            localStorage.setItem("id", json.userId);
            return { success: true, message:"ok", data: json };
        } catch (error) {
            console.error(error);
            return { success: false, message: error.message };
        }
    },
    async getGames() {
        const accessToken = localStorage["accessToken"];
        try {
            const response = await fetch(`${API_URL}/games/games`, {
                headers: { Authorization: `Bearer ${accessToken}` },
            });

            if (!response.ok) {
                return { success: false, message: `HTTP error! status: ${response.status}` };
            }

            const json = await response.json();
            return { success: true, data: json };
        } catch (error) {
            console.error(error);
            return { success: false, message: error.message };
        }
    }
}

export default apiService;