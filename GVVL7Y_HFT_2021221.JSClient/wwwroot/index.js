let gitUsers = [];
let connection = null;
let idtoedit=-1;

getData();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:39621/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GitUserCreated", (user, message) => {
        getData();
    });
    connection.on("GitUserDeleted", (user, message) => {
        getData();
    });
    connection.on("GitUserUpdated", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function getData() {

    await fetch('http://localhost:39621/gitUser')
        .then(x => x.json())
        .then(y => {
            gitUsers = y;
            console.log(gitUsers);
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    gitUsers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id +
            "</td><td>" + t.name +
            "</td><td>" + t.emailContact +
        "</td><td>" + `<button type="button" class="delete" onclick="remove(${t.id})">Delete</button>`
        + `<button type="button" class="edit" onclick="showedit(${t.id})">Edit</button>` + "</td></tr>";
    })
}

function showedit(id) {
    //alert(id);
    document.getElementById('name_edit').value = gitUsers.find(t => t['id'] == id)['name'];
    document.getElementById('emailContact_edit').value = gitUsers.find(t => t['id'] == id)['emailContact'];
    document.getElementById('editformdiv').style.display = 'flex';
    idtoedit = id;
}


function edit() {
    //document.getElementById('editformdiv').style.display = 'none';
    let _name = document.getElementById('name_edit').value;
    let _emailContact = document.getElementById('emailContact_edit').value;
    if (_emailContact == '') {
        _emailContact = null;
    }
    fetch('http://localhost:39621/gitUser', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: _name,
                emailContact: _emailContact,
                id:idtoedit
                //orderCount: "20",
                //orderName: "Fogzománcvédő",
                //orderPrice: "1000"
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });

}

function remove(id) {
    fetch('http://localhost:39621/gitUser/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let _name = document.getElementById('name_input').value;
    let _emailContact = document.getElementById('emailContact_input').value;
    if (_emailContact=='') {
        _emailContact = null;
    }
    fetch('http://localhost:39621/gitUser', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: _name,
                emailContact: _emailContact
                //orderCount: "20",
                //orderName: "Fogzománcvédő",
                //orderPrice: "1000"
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {console.error('Error:', error);});
    
}