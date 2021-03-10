const state = {
    rules: {
        required: value => !!value || "Campo Requerido",
        email: value => {
            const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            return pattern.test(value) || "Email no es Valido"
        },
        min8Characters: value => value.length >= 8 || "El valor debe ser de mas de 8 caracteres"
    }
};

const getters = {
    getRules: (state) => state.rules
};

export default {
    state, getters
}
