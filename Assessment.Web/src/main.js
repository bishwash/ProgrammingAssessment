import Vue from 'vue'
import BootstrapVue from "bootstrap-vue"
import App from './App.vue'
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-vue/dist/bootstrap-vue.css"
import VueRouter from 'vue-router'

Vue.use(BootstrapVue)
Vue.use(VueRouter)
Vue.prototype.$eventHub = new Vue();


new Vue({
    el: '#app',
    render: h => h(App)
})
