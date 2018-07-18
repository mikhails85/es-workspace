<template>
    <div class="container-fluid">
        <div class="animated fadeIn">
            <b-card>
                <div slot="header">
                    <strong>Skill</strong>
                </div>
                <b-form @submit="onAddSkill">
                    <b-form-group id="skillNameGroup"
                        label="Skill Name:"
                        label-for="skillNameInput">
                        <b-form-input id="skillNameInput"
                            type="text"
                            v-model="skill.name"
                            required
                            placeholder="Enter skill name">
                        </b-form-input>
                    </b-form-group>
                     <b-button type="submit" variant="primary"><i class="fa fa-plus-circle"></i>&nbsp;Skill</b-button>
                </b-form>
                <b-form inline @submit="onSearch">
                    <b-form-group id="searchGroup"
                        label="Search:"
                        label-for="searchInput">
                        <b-form-input id="searchInput"
                            type="text"
                            v-model="search"                            
                            placeholder="Search">
                        </b-form-input>
                    </b-form-group>
                    <b-button type="submit" variant="primary"><i class="fa fa-search"></i>&nbsp;Search</b-button>
                </b-form>

               <b-table striped hover :items="skills.items" :fields="skills.fields"></b-table>
                <b-row class="text-center">                    
                    <b-col cols="12">
                        <b-button variant="primary" @click="NextPage">More</b-button>                
                    </b-col>                    
                </b-row>                
               </div>
            </b-card>
        </div>
    </div>
</template>

<script>

export default {
    components: {
        BaseInputText, TodoListItem
    },
    data () {
        return {
            newTodoText: '',
            todos: [
                {
                    id: nextTodoId++,
                    text: 'Learn Vue'
                },
                {
                    id: nextTodoId++,
                    text: 'Learn about single-file components'
                },
                {
                    id: nextTodoId++,
                    text: 'Fall in love'
                }
            ]
        }
    },
    methods: {
        addTodo () {
            const trimmedText = this.newTodoText.trim()
            if (trimmedText) {
                this.todos.push({
                    id: nextTodoId++,
                    text: trimmedText
                })
                this.newTodoText = ''
            }
        },
        removeTodo (idToRemove) {
            this.todos = this.todos.filter(todo => {
                return todo.id !== idToRemove
            })
        }
    },
    beforeMount(){
        this.getUnits()
    }
}
</script>