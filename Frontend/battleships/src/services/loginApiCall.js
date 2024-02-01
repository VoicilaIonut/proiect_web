/*export default async function login(obj) {
    const url = "http://localhost:5275/api/users/login";

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
            //localStorage.setItem("refreshToken", json["refreshToken"]);
        }

        return response;
    } catch (e) {
        console.error('An error occurred during the login process', e);
        return false;
    }
}*/