<template>
  <div class="animated fadeIn">
    <div class="card">
      <div class="card-header">
        Table
      </div>
      <div class="card-body">
        <v-table ref="table" :tblData="items" :tblFields="fields" :onCreate="create" :onDelete="remove" ></v-table>        
        <b-modal id="modalAdd" @hide="resetAddModal" @ok="addItem" title="New item">
            <v-form :frm-model="modalAdd" :frm-fields="modelFields"></v-form>
        </b-modal>
      </div>  
    </div>
  </div>
</template>
<script>
import axios from 'axios'
import table from '../controls/Table.vue'
import form from '../controls/Form.vue'
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
                  { key: 'id', label: 'ID', sortable: true },
                  { key: 'name', label: 'Name', sortable: true }                  
              ],
      modelFields:[          
          {key:'name', id:'SkillName', type:'text', label:'Skill', placeholder:"Enter skill" }
      ],      
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
        console.log(JSON.stringify(this.modalAdd, null, 2))     
        axios.post('/api/skills/addskill', { Name : this.modalAdd.name} )
        this.refresh()
        this.$root.$emit('bv::hide::modal', 'modalAdd', null)
    },
    
    create(){      
      this.$root.$emit('bv::show::modal', 'modalAdd', null)
    },    
    remove(item){
      console.log('delete item')
      console.log(JSON.stringify(item, null, 2))
    },
    refresh(){
      let self = this  
      axios.get('/api/skills/list').then(r=>{          
          if(r.data.success)
          {  
              self.$refs.table.refresh(r.data.value);
          }
      })  
    }
  },
  mounted () {
     this.refresh()       
  }
}
</script>
