let user = { name: 'Masha', age: 21 };
let numbers = [1, 2, 3];
let user1 = {
    name: 'Masha', age: 23,
    location: { city: 'Minsk', country: 'Belarus' }
};
let user2 = {
    name: 'Masha', age: 28,
    skills: ["HTML", "CSS", "JavaScript", "React"]
};
let _arr = { id1: 10 };
const array = [
    { id: 1, name: 'Vasya', group: 10 },
    { id: 2, name: 'Ivan', group: 11 },
    { id: 3, name: 'Masha', group: 12 },
    { id: 4, name: 'Petya', group: 10 },
    { id: 5, name: 'Kira', group: 11 }
];
let user4 = {
    name: 'Masha', age: 19,
    studies: {
        university: 'BSTU', speciality: 'designer', year: 2020,
        exams: { maths: true, programming: false }
    }
};
let user5 = {
    name: 'Masha', age: 22,
    studies: {
        university: 'BSTU', speciality: 'designer', year: 2020,
        department: { faculty: 'FIT', group: 10 },
        exams: [
            { maths: true, mark: 8 },
            { programming: true, mark: 4 }
        ]
    }
};
let user6 = {
    name: 'Masha', age: 21,
    studies: {
        university: 'BSTU', speciality: 'designer', year: 2020,
        department: { faculty: 'FIT', group: 10 },
        exams: [
            { maths: true, mark: 8, professor: { name: 'Ivan Ivanov', degree: 'PhD' } },
            { programming: true, mark: 10, professor: { name: 'Petr Petrov', degree: 'PhD' } }
        ]
    }
};
let user7 = {
    name: 'Masha', age: 20,
    studies: {
        university: 'BSTU', speciality: 'designer', year: 2020,
        department: { faculty: 'FIT', group: 10 },
        exams: [
            {
                maths: true, mark: 8,
                professor: {
                    name: 'Ivan Petrov', degree: 'PhD',
                    articles: [
                        { title: "About HTML", pagesNumber: 3 },
                        { title: "About CSS", pagesNumber: 5 },
                        { title: "About JavaScript", pagesNumber: 1 }
                    ]
                }
            },
            {
                programming: true, mark: 10,
                professor: {
                    name: 'Petr Ivanov', degree: 'PhD',
                    articles: [
                        { title: "About HTML", pagesNumber: 3 },
                        { title: "About CSS", pagesNumber: 5 },
                        { title: "About JavaScript", pagesNumber: 1 }
                    ]
                }
            }
        ]
    }
};
let store = {
    state: {
        profilePage: {
            posts: [
                { id: 1, message: 'Hi', likesCount: 12 },
                { id: 2, message: 'By', likesCount: 1 }
            ],
            newPostText: 'About me'
        },
        dialogsPage: {
            dialogs: [
                { id: 1, name: 'Valera' },
                { id: 2, name: 'Andrey' },
                { id: 3, name: 'Sasha' },
                { id: 4, name: 'Viktor' }
            ],
            messages: [
                { id: 1, message: 'hi' },
                { id: 2, message: 'hi hi' },
                { id: 3, message: 'hi hi hi' }
            ]
        },
        sidebar: []
    }
};
let userCopy = Object.assign({}, user);
let numbersCopy = [...numbers];
let user1Copy = Object.assign(Object.assign({}, user1), { location: Object.assign({}, user1.location) });
let user2Copy = Object.assign(Object.assign({}, user2), { skills: [...user2.skills] });
let arrayCopy = array.map(item => (Object.assign({}, item)));
let user4Copy = Object.assign(Object.assign({}, user4), { studies: Object.assign(Object.assign({}, user4.studies), { exams: Object.assign({}, user4.studies.exams) }) });
let user5Copy = Object.assign(Object.assign({}, user5), { studies: Object.assign(Object.assign({}, user5.studies), { department: Object.assign({}, user5.studies.department), exams: user5.studies.exams.map(exam => (Object.assign({}, exam))) }) });
let user6Copy = Object.assign(Object.assign({}, user6), { studies: Object.assign(Object.assign({}, user6.studies), { department: Object.assign({}, user6.studies.department), exams: user6.studies.exams.map(exam => (Object.assign(Object.assign({}, exam), { professor: exam.professor ? Object.assign({}, exam.professor) : undefined }))) }) });
let user7Copy = Object.assign(Object.assign({}, user7), { studies: Object.assign(Object.assign({}, user7.studies), { department: Object.assign({}, user7.studies.department), exams: user7.studies.exams.map(exam => (Object.assign(Object.assign({}, exam), { professor: exam.professor ? Object.assign(Object.assign({}, exam.professor), { articles: exam.professor.articles ? exam.professor.articles.map(article => (Object.assign({}, article))) : undefined }) : undefined }))) }) });
let copyUser7 = Object.assign(Object.assign({}, user7), { studies: Object.assign(Object.assign({}, user7.studies), { department: Object.assign({}, user7.studies.department), exams: user7.studies.exams.map(exam => (Object.assign(Object.assign({}, exam), { professor: exam.professor ? Object.assign(Object.assign({}, exam.professor), { articles: exam.professor.articles ? exam.professor.articles.map(article => (Object.assign({}, article))) : undefined }) : undefined }))) }) });
let storeCopy = Object.assign(Object.assign({}, store), { state: Object.assign(Object.assign({}, store.state), { profilePage: Object.assign(Object.assign({}, store.state.profilePage), { posts: store.state.profilePage.posts.map(post => (Object.assign(Object.assign({}, post), { message: "Hello" }))) }), dialogsPage: Object.assign(Object.assign({}, store.state.dialogsPage), { messages: store.state.dialogsPage.messages.map(msg => (Object.assign(Object.assign({}, msg), { message: "Hello" }))) }) }) });
user5Copy.studies.department.group = 12;
const exam5 = user5Copy.studies.exams.find(exam => exam.programming);
if (exam5)
    exam5.mark = 10;
const exam6 = user6Copy.studies.exams.find(exam => exam.programming);
if (exam6 && exam6.professor)
    exam6.professor.name = 'New Professor Name';
const exam7 = user7Copy.studies.exams.find(exam => exam.professor && exam.professor.name === 'Petr Ivanov');
if (exam7 && exam7.professor && exam7.professor.articles) {
    const art = exam7.professor.articles.find(article => article.title === 'About CSS');
    if (art)
        art.pagesNumber = 3;
}
console.log(userCopy);
export {};
//Ctrl + ~
//tsc lab_1.ts --target es6
//node lab_1.js
