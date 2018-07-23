<template>
  <div class="animated fadeIn">
    <div class="card">
      <div class="card-header">
        Table
      </div>
      <div class="card-body">
        <v-table :tblData="items" :tblFields="fields" :onCreate="create" :onDelete="remove" ></v-table>        
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
  name: 'Skills',
  components: {
    'v-table':table,
    'v-form':form
  },
  data(){
    return {
      items : [],
      fields: [
                  { key: 'Id', label: 'ID', sortable: true },
                  { key: 'Name', label: 'Name', sortable: true }                                      
              ],
      modelFields:[          
          {key:'name', id:'SkillName', type:'text', label:'Skill', placeholder:"Enter skill" }
      ],
      modalEdit: { id: 0, name: ''},
      modalAdd: { id: 0, name: ''}
    }
  },
  methods:{
    resetEditModal () {
        this.modalEdit.id = 0
        this.modalEdit.name = ''
    },
    resetAddModal () {
        this.modalAdd.id = 0
        this.modalAdd.name = ''
    },    
    addItem(evt){
        evt.preventDefault();  
        axios.post('/api/skills/addskill',this.addItem)
        this.$root.$emit('bv::hide::modal', 'modalAdd', null)
    },
    
    create(){      
      this.$root.$emit('bv::show::modal', 'modalAdd', null)
    },    
    remove(item){
      console.log('delete item')
      console.log(JSON.stringify(item, null, 2))
    }
  },
  created() {
      axios.get('/api/skills/list').then(r=>{
          if(r.data.Success)
          {
              this.items = r.data.Value
          }
      })        
  }
}
</script>
