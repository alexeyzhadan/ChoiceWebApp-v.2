const selectDiscElement = document.querySelectorAll('.select-disc');

selectDiscElement.forEach(s => {
    s.addEventListener('change', (event) => {
        selectPostAjax(event.target.value);
    });
});

async function selectPostAjax(value) {
    const url = '/home/select';
    let xsrf_token = document.getElementsByName('__RequestVerificationToken')[0].value;
    try {
        const response = await fetch(url, {
            method: 'POST',
            credentials: 'include',
            headers: {
                'XSRF-TOKEN': xsrf_token,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(value)
        });
        if (response.status === 200) {
            const json = await response.json();
            console.log(json.message);
        }
        else {
            const text = await response.text();
            alert(text);
        }
    } catch (error) {
        console.log('Error:', error);
    }
}