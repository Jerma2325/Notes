const saveEventButton = document.querySelector('#btnSave-E');
const addEventButton = document.querySelector('#btnAdd-E');
const deleteEventButton = document.querySelector('#btnDelete-E');
const eventTitleInput = document.querySelector('#title-E');
const eventDescriptionInput = document.querySelector('#description-E');
const eventStartDateInput = document.querySelector('#dateStart');
const eventEndDateInput = document.querySelector('#dateEnd');
const eventsContainer = document.querySelector('#events__container');

let selectedEventId = null;


function clearEventForm() {
  eventTitleInput.value = '';
  eventDescriptionInput.value = '';
  eventStartDateInput.value = '';
  eventEndDateInput.value = '';
  deleteEventButton.classList.add('hidden');
  saveEventButton.classList.add('hidden');
  selectedEventId = null;
}


function displayEventInForm(event) {
  eventTitleInput.value = event.title;
  eventDescriptionInput.value = event.description;
  eventStartDateInput.value = event.startDate.slice(0, 10);
  eventEndDateInput.value = event.endDate.slice(0, 10);
  deleteEventButton.classList.remove('hidden');
  saveEventButton.classList.remove('hidden');
  selectedEventId = event.id;
}


function getEventById(id) {
  fetch(`https://localhost:7202/api/event111s/${id}`)
    .then(response => response.json())
    .then(data => displayEventInForm(data))
    .catch(error => console.log(error));
}


function getAllEvents() {
  fetch('https://localhost:7202/api/event111s')
    .then(response => response.json())
    .then(data => displayEvents(data))
    .catch(error => console.log(error));
}


function displayEvents(events) {
  let allEvents = '';

  events.forEach(event => {
    const eventElement = `
      <div class="event" data-id="${event.id}">
        <h3>${event.title}</h3>
        <p>${event.description}</p>
        <p><strong>Start Date:</strong> ${event.startDate.slice(0, 10)}</p>
        <p><strong>End Date:</strong> ${event.endDate.slice(0, 10)}</p>
      </div>
    `;
    allEvents += eventElement;
  });

  eventsContainer.innerHTML = allEvents;


  document.querySelectorAll('.event').forEach(event => {
    event.addEventListener('click', function () {
      const eventId = event.dataset.id;
      getEventById(eventId);
    });
  });
}


addEventButton.addEventListener('click', function () {
  const title = eventTitleInput.value;
  const description = eventDescriptionInput.value;
  const startDate = eventStartDateInput.value;
  const endDate = eventEndDateInput.value;

  if (selectedEventId) {
    updateEvent(selectedEventId, title, description, startDate, endDate);
  } else {
    addEvent(title, description, startDate, endDate);
  }
});


function addEvent(title, description, startDate, endDate) {
  const body = {
    title: title,
    description: description,
    startDate: startDate,
    endDate: endDate,
    isVisable: true
  };

  fetch('https://localhost:7202/api/event111s', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(body)
  })
    .then(response => response.json())
    .then(data => {
      clearEventForm();
      getAllEvents();
    })
    .catch(error => console.log(error));
}


function updateEvent(id, title, description, startDate, endDate) {
  const body = {
    title: title,
    description: description,
    startDate: startDate,
    endDate: endDate,
    isVisable: true
  };

  fetch(`https://localhost:7202/api/event111s/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(body)
  })
    .then(response => response.json())
    .then(data => {
      clearEventForm();
      getAllEvents();
    })
    .catch(error => console.log(error));
}


function deleteEvent(id) {
  fetch(`https://localhost:7202/api/event111s/${id}`, {
    method: 'DELETE'
  })
    .then(response => {
      clearEventForm();
      getAllEvents();
    })
    .catch(error => console.log(error));
}


deleteEventButton.addEventListener('click', function () {
  if (selectedEventId) {
    deleteEvent(selectedEventId);
  }
});


saveEventButton.addEventListener('click', function () {
  if (selectedEventId) {
    saveEvent();
  }
});


function saveEvent() {
  const title = eventTitleInput.value;
  const description = eventDescriptionInput.value;
  const startDate = eventStartDateInput.value;
  const endDate = eventEndDateInput.value;

  updateEvent(selectedEventId, title, description, startDate, endDate);
}


function handleEventClick() {
  saveEventButton.classList.remove('hidden');
  deleteEventButton.classList.remove('hidden');
}


eventsContainer.addEventListener('click', handleEventClick);


getAllEvents();
