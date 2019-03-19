<template>
    <div>
        <form method="post"  @submit.prevent="postStudent">
            <h4>Add New Student</h4>
            <div v-if="message" style="text-align: center;color:red"><b>{{message}}</b></div>
            <div class="form-group form-inline">
                <label for="firstName" class="col-md-2">First Name</label>
                <input type="text" id="firstName" v-model="input.firstName" class="form-control" placeholder="First Name" />
            </div>
            <div class="form-group form-inline">
                <label for="lastName" class="col-md-2">Last Name</label>
                <input type="text" id="lastName" v-model="input.lastName" class="form-control" placeholder="Last Name" />
            </div>
            <div class="form-group form-inline">
                <label for="dob" class="col-md-2">Date of birth</label>
                <input type="text" id="dob" v-model="input.dob" class="form-control" placeholder="Date of birth" />
            </div>
            <div class="form-group form-inline">
                <label for="gpa" class="col-md-2">GPA</label>
                <input type="text" id="gpa" v-model="input.gpa" class="form-control" placeholder="GPA" />
            </div>
            <button type="submit" class="btn btn-primary">Save</button>
        </form>
        <br/><br />
        <studentList></studentList>
    </div>
</template>
<script>
    import axios from "axios";
    import StudentList from './StudentList.vue'
    export default {
        name: 'Student',
        data() {
            return {
                message: "",
                input: {
                    firstName: "",
                    lastName: "",
                    dob: "",
                    gpa: ""
                },
                response: ""
            }
        },
        methods: {
            
            postStudent: function () {
                var vm = this;
                console.log("test");
                axios
                    .post('http://localhost:49627/api/v1/students', 
                       {
                            firstName: this.input.firstName,
                            lastName: this.input.lastName,
                            gpa: this.input.gpa,
                            dob: this.input.dob
                    }).then(function (response) {
                        vm.$forceUpdate();
                        vm.message = 'Student Added successfully';
                    })
                    .catch(function (error) {
                        if (error.response.status == 400) {
                            vm.message = "Error while inserting student. Please check your data and try again.";
                            vm.message = JSON.parse(error.response.data)
                        }
                        if (!error.response) {
                            // network error
                            vm.message = 'Error: Network Error';
                        } else {
                            vm.message = error.response.data.message;
                        }
                    })
            }
        },
        components: {
            StudentList
        }
    }
</script>

<style scoped>
    h1, h2 {
        font-weight: normal;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        display: inline-block;
        margin: 0 10px;
    }

    a {
        color: #42b983;
    }

    textarea {
        width: 600px;
        height: 200px;
    }
</style>