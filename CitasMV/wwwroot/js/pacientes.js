// pacientes.js
// Script para selects dependientes: departamentos, ciudades y barrios

function fillSelect(selectEl, items, placeholder) {
    selectEl.innerHTML = "";
    const opt = document.createElement("option");
    opt.value = "";
    opt.text = placeholder || "-- Seleccione --";
    selectEl.appendChild(opt);

    items.forEach(i => {
        const o = document.createElement("option");
        o.value = i.value;
        o.text = i.text;
        selectEl.appendChild(o);
    });
}

document.addEventListener("DOMContentLoaded", function () {

    const dptoResid = document.getElementById("dptoResidencia");
    const ciudadResid = document.getElementById("ciudadResidencia");
    const barrioResid = document.getElementById("barrioResidencia");

    const dptoNaci = document.getElementById("dptoNacimiento");
    const ciudadNaci = document.getElementById("ciudadNacimiento");

    // RESIDENCIA: dpto → ciudades
    if (dptoResid) {
        dptoResid.addEventListener("change", function () {
            const dpto = this.value;

            fetch(`/Pacientes/GetCiudades?dpto=${dpto}`)
                .then(r => r.json())
                .then(data => {
                    fillSelect(ciudadResid, data.map(x => ({ value: x.value, text: x.text })), "-- Seleccione ciudad --");
                    fillSelect(barrioResid, [], "-- Seleccione barrio --");
                });
        });
    }

    // RESIDENCIA: ciudad → barrios
    if (ciudadResid) {
        ciudadResid.addEventListener("change", function () {
            const ciudad = this.value;

            fetch(`/Pacientes/GetBarrios?ciudad=${ciudad}`)
                .then(r => r.json())
                .then(data => {
                    fillSelect(barrioResid, data.map(x => ({ value: x.value, text: x.text })), "-- Seleccione barrio --");
                });
        });
    }

    // NACIMIENTO: dpto → ciudades
    if (dptoNaci) {
        dptoNaci.addEventListener("change", function () {
            const dpto = this.value;

            fetch(`/Pacientes/GetCiudades?dpto=${dpto}`)
                .then(r => r.json())
                .then(data => {
                    fillSelect(ciudadNaci, data.map(x => ({ value: x.value, text: x.text })), "-- Seleccione ciudad --");
                });
        });
    }

});
