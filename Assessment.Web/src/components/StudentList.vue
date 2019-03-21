<template>
    <div class="overflow-auto">

        <div>
            <div v-if="!hasRecords" style="text-align: center"><br><br>LOADING DATA...</div>
            <div v-if="hasRecords">
                <b-row>
                    <b-col md="6" class="my-1">
                        <b-form-group horizontal label="Per page" class="mb-0">
                            <b-form-select :options="pageOptions" v-model="perPage" />
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-table :items="records" :fields="column" striped hover :current-page="currentPage" :per-page="perPage">
                    <template slot="HEAD_selected" slot-scope="data">
                        <input type="checkbox" @click.stop v-model="selectAll" @change="toggleSelectAll" />
                    </template>
                </b-table>
                <b-row>
                    <b-col md="6" class="my-1">
                        <b-pagination :total-rows="totalRows" :per-page="perPage" v-model="currentPage" class="my-0" />
                    </b-col>
                </b-row>
            </div>
        </div>
        
    </div>
</template>

<script>
    
   
    import axios from "axios";
    
    export default {
        data() {
            return {
                selectAll: false,
                records: [],
                perPage: 10,
                currentPage: 1,
                pageOptions: [5, 10, 15],
                column: [
                    {
                        key: "firstName",
                        sortable: true,
                        label: "First Name"
                    },
                    {
                        key: "lastName",
                        sortable: true,
                        label: "Last Name",
                        class: "text-right options-column"
                    },
                    {
                        key: "dob",
                        sortable: true,
                        label: "Date of birth",
                        class: "text-right options-column"
                    },
                    {
                        key: "gpa",
                        sortable: false,
                        label: "GPA",
                        class: "options-column"
                    }
                ]
            };
        },
        computed: {
            hasRecords() {
                return this.records.length > 0;
            },
            totalRows() {
                return this.records.length;
            }
        },
        methods: {
            selectRow(item) {
                if (item.selected) {
                    item._rowVariant = "info";
                } else {
                    item._rowVariant = "default";
                    if (this.selectAll) {
                        this.selectAll = false;
                    }
                }
            },
            toggleSelectAll() {
                if (this.selectAll) {
                    for (var i = 0; i < this.records.length; i++) {
                        this.records[i].selected = true;
                        this.records[i]._rowVariant = "info";
                    }
                } else {
                    for (var i = 0; i < this.records.length; i++) {
                        this.records[i].selected = false;
                        this.records[i]._rowVariant = "default";
                    }
                }
            },
            getResults(ctx, callback) {
                axios.get('http://localhost:7000/api/v1/students')

                    .then(response => {
                        console.log(response.data);
                        this.records = response.data;
                        this.records.length = response.data.length;
                        return this.records;
                    });
            }
        },
        components: {},
        mounted() {
            this.getResults();
           
        },
        created() {
            this.$eventHub.$on('student-added', this.getResults);
        },
        beforeDestroy() {
            this.$eventHub.$off('student-added');
        }
    };
</script>