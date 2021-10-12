var indwxVm = new Vue({
    el: '#app',
    data: {
        HomeDT: null,
        homes: [],
        query: { Beds: null, Baths: null, Cities: null, States: null, PropertyTypes: null, MinPrice: null, MaxPrice: null, MinSquareFeet: null, MaxSquareFeet: null },
        filters: { beds: [], baths: [] }
    },
    methods: {
        async loadData() {
            var types = [];
            $("input[type='checkbox']:checked").each(function () {
                var val = $(this).val();
                types.push("'" + val + "'");
            });
            this.query.PropertyTypes = types.join(",");
            var resp = await axios.get('/api/Home/Homes', { params: this.query })
            var rows = resp.data;

            this.homes = rows;
            this.HomeDT.clear().draw();
            this.HomeDT.rows.add(rows);
            this.HomeDT.columns.adjust().draw();
        },
        loadFilters() {
            axios.get('/api/Home/Cities')
                .then(async resp => {
                    this.filters.Cities = resp.data;
                    this.$forceUpdate();
                })
            axios.get('/api/Home/States')
                .then(async resp => {
                    this.filters.States = resp.data;
                    this.$forceUpdate();
                })
            axios.get('/api/Home/PropertyTypes')
                .then(async resp => {
                    this.filters.PropertyTypes = resp.data;
                    this.$forceUpdate();
                })
        },
        decorateFilters(rows) {
            this.filters.beds = rows.map(x => x.Beds).filter((v, index, x) => x.indexOf(v) === index).sort((a, b) => { return a - b });
            this.filters.baths = rows.map(x => x.Baths).filter((v, index, x) => x.indexOf(v) === index).sort((a, b) => { return a - b });
        },
        ImportFile() {
            // read file
            let file = $("#fileInput").get(0).files[0];
            let formData = new FormData();
            formData.append("file", file);

            axios.post('/api/Home/Import', formData, { headers: { 'Content-Type': 'multipart/form-data' } })
                .then(async resp => {
                    alert(resp.data);
                    await this.loadData();
                    this.decorateFilters(this.homes);

                    $("#fileInput").val(null);
                })
        }
    },
    mounted() {
        this.HomeDT = $('#homeTable').DataTable({
            destroy: true,
            columns: [
                {
                    data: 'Address',
                    render: function (data, type, row, meta) {
                        return type === 'display' && data != "" ?
                            row["Address"] + ', ' + row["City"] + ', ' + row["State"] + ', ' + row["Zip"] :
                            data;
                    }
                },
                { data: 'Beds' },
                { data: 'Baths' },
                { data: 'Price' },
                { data: 'PropertyType' },
                { data: 'SquareFeet' }
            ]
        });
    },
    async created() {
        await this.loadData();
        this.loadFilters();
        this.decorateFilters(this.homes);
    }
})