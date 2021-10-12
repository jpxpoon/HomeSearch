var indwxVm = new Vue({
    el: '#app',
    data: {
        message: 'You loaded this page on ' + new Date().toLocaleString(),
        homes: []
    },
    methods: {
        loadData() {
            axios.get('/api/Homes/GetHomes')
                .then(resp => {
                    this.homes = resp
                })
        }
    },
    created() {
        this.loadData()
    }
})