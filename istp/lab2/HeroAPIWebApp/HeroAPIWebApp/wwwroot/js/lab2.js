const uri = 'api/Skills';
let skills = [];

function getSkills() {
    console.log("getSkills");
    fetch(uri)
        .then(response => response.json())
        .then(data => _displaySkills(data))
        .catch(error => console.error('Unable to get skills.', error));
}

function addSkill() {
    console.log("addSkill");
    const addNameTextbox = document.getElementById('add-name');
    const addDescriptionTextbox = document.getElementById('add-description');
    const skill = {
        name: addNameTextbox.value.trim(),
        description: addDescriptionTextbox.value.trim(),
    };
    fetch(uri, {
        method: 'POST',
        headers: {

            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(skill)
    })
        .then(response => response.json())
        .then(() => {
            getSkills();
            addNameTextbox.value = '';
            addDescriptionTextbox.value = '';
        })
        .catch(error => console.error('Unable to add skill.', error));
}

function deleteSkill(id) {
    console.log("deleteSkill", id);
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getSkills())
        .catch(error => console.error('Unable to delete skill.', error));
}

function displayEditForm(id) {
    console.log("displayEditForm", id);
    const skill = skills.find(skill => skill.id === id);
    //if (skill == undefined) return;
    document.getElementById('edit-id').value = skill.id;
    document.getElementById('edit-name').value = skill.name;
    document.getElementById('edit-description').value = skill.description;
    document.getElementById('editSkill').style.display = 'block';
}

function updateSkill() {
    console.log("updateSkill");
    const skillId = document.getElementById('edit-id').value;
    const skill = {
        id: parseInt(skillId, 10),
        name: document.getElementById('edit-name').value.trim(),
        description: document.getElementById('edit-description').value.trim()
    };
    fetch(`${uri}/${skillId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(skill)
    })
        .then(() => getSkills())
        .catch(error => console.error('Unable to update skill.', error));
    closeInput();
    return false;
}

function closeInput() {
    document.getElementById('editSkill').style.display = 'none';
}

function _displaySkills(data) {
    console.log("_displaySkills");
    const tBody = document.getElementById('skills');

    tBody.innerHTML = '';
    const button = document.createElement('button');
    data.forEach(skill => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${skill.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteSkill(${skill.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(skill.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(skill.description);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    skills = data;
}