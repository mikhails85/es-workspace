<template>
  <div class="animated fadeIn">
    <div class="card">
      <div class="card-header">
        Table
      </div>
      <div class="card-body">
        <v-table :tblData="items" :tblFields="fields" :onNextPage="getNextPage" :onCreate="create" :onUpdate="update" :onDelete = "remove" :onSearch = "search"></v-table>
        <b-modal id="modalEdit" @hide="resetEditModal" @ok="saveChanges" title="Edit item">
            <v-form :frm-model="modalEdit" :frm-fields="modelFields"></v-form>
        </b-modal>
        <b-modal id="modalAdd" @hide="resetAddModal" @ok="addItem" title="New item">
            <v-form :frm-model="modalAdd" :frm-fields="modelFields"></v-form>
        </b-modal>
      </div>  
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import table from './controls/Table.vue'
import form from './controls/Form.vue'
export default {
  name: 'HelloWorld',
  components: {
    'v-table':table,
    'v-form':form
  },
  data(){
    return {
      items : [
                { age: 40, name: { first: 'Dickerson', last: 'Macdonald' } },
                { age: 21, name: { first: 'Larsen', last: 'Shaw' } },  
                { age: 89, name: { first: 'Geneva', last: 'Wilson' } },
                { age: 38, name: { first: 'Jami', last: 'Carney' } },
                { age: 27, name: { first: 'Essie', last: 'Dunlap' } },
                { age: 40, name: { first: 'Thor', last: 'Macdonald' } }, 
                { age: 26, name: { first: 'Mitzi', last: 'Navarro' } },
                { age: 22, name: { first: 'Genevieve', last: 'Wilson' } },
                { age: 38, name: { first: 'John', last: 'Carney' } },
                { age: 29, name: { first: 'Dick', last: 'Dunlap' } }
              ],
      fields: [
                  { key: 'name', label: 'Person Full name', sortable: true },
                  { key: 'age', label: 'Person age', sortable: true },                    
                  { key: 'actions', label: 'Actions', class:"sm" }
              ],
      modelFields:[
          {key:'age', id:'age', type:'number', label:'Person age', placeholder:"Enter person age" },
          {key:'first_name', id:'first_name', type:'text', label:'Person first name', placeholder:"Enter person first name" },
          {key:'last_name', id:'last_name', type:'text', label:'Person last name', placeholder:"Enter person last name" },
      ],
      modalEdit: { age: 0, first_name: '', last_name:'' },
      modalAdd: { age: 0, first_name: '', last_name:'' }
    }
  },
  methods:{
    resetEditModal () {
        this.modalEdit.age = 0
        this.modalEdit.first_name = ''
        this.modalEdit.last_name = ''
    },
    resetAddModal () {
        this.modalAdd.age = 0
        this.modalAdd.first_name = ''
        this.modalAdd.last_name = ''
    },
    saveChanges(evt){
        evt.preventDefault()        
        console.log('update item')
        console.log(JSON.stringify(this.modalEdit, null, 2))
        this.$root.$emit('bv::hide::modal', 'modalEdit', null)
    },
    addItem(evt){
        evt.preventDefault();  
        console.log('create item')       
        console.log(JSON.stringify(this.modalAdd, null, 2))     
        this.$root.$emit('bv::hide::modal', 'modalAdd', null)
    },
    getNextPage(){
      console.log('next page')
    },
    create(){      
      this.$root.$emit('bv::show::modal', 'modalAdd', null)
    },
    update(item){      
      this.modalEdit.age = item.age
      this.modalEdit.first_name = item.name.first
      this.modalEdit.last_name = item.name.last
      this.$root.$emit('bv::show::modal', 'modalEdit', null)
    },
    remove(item){
      console.log('delete item')
      console.log(JSON.stringify(item, null, 2))
    },
    search(text){
      console.log('search: '+ text)
    }
  }
}
</script>

