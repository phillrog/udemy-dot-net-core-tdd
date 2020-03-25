import { Selector } from 'testcafe';

fixture('Servidor')
    .page('localhost:5000')

test('Validando se esta de pé', async t => {
    await t.expect(Selector('title').innerText).eql('Home Page - CursoOnline')
})

